
using BarberBoss.Application.UseCases.Reports.Fonts;
using BarberBoss.Domain.Bills;
using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Reports;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Reports
{
    public class GenerateExpenseReportPdfUseCase : IGenerateExpenseReportPdfUseCase
    {
        private const string CURRECY_SYMBOL = "R$";
        private const int HEIGHT_ROW_BILL = 25;
        private readonly IBillReadOnlyRepository _repository;


        public GenerateExpenseReportPdfUseCase(IBillReadOnlyRepository repository)
        {
            _repository = repository;

            GlobalFontSettings.FontResolver = new BillReportFontResolver();
        }



        public async Task<byte[]> Execute(DateOnly date)
        {
            var bilss = await _repository.GetByMonth(date);

            if(bilss.Count == 0)
            {
                return [];
            }

            var document = CreateDocument(date);
            document.Info.Title = $"{ResourceReportMessages.DESPESAPARA} {date.ToString("Y")}";

            document.Info.Author = "Wagner Morais";

            var style = document.Styles["Normal"];
            // Se não especificarm,sera essa fonte
            style!.Font.Name = FontHelper.RALEWAY_BLACK;

            var page = CreatePage(document);
            CreateHeaderWithProfilePhotoAndName(page);
            var totalBills = Math.Round(bilss.Sum(bill => bill.Amount), 2);

            CreateTotalSpentSection(page, date, totalBills);

            foreach (var bill in bilss)
            {
                var table = CreateBillTable(page);

                var row = table.AddRow();
                row.Height = HEIGHT_ROW_BILL;

                AddBillDescription(row.Cells[0], bill.Description);

                AddHeaderAmount(row.Cells[3]);

                row = table.AddRow();
                row.Height = HEIGHT_ROW_BILL;

                row.Cells[0].AddParagraph(bill.Date.ToString("D"));
                SetStyleBaseForBillInformation(row.Cells[0]);
                row.Cells[0].Format.LeftIndent = 15;

                row.Cells[1].AddParagraph(bill.Date.ToString("t"));
                SetStyleBaseForBillInformation(row.Cells[1]);

                row.Cells[2].AddParagraph(bill.PaymentType.PaymentTypeToString());
                SetStyleBaseForBillInformation(row.Cells[2]);

                AddAmountForBill(row.Cells[3] , bill.Amount);

                AddWhiteSpace(table);


            }



            return RenderDocument(document);
        }



        private Document CreateDocument(DateOnly month)
        {
            var document = new Document();

            return document;
        }


        private Section CreatePage(Document document)
        {
            // Adiciona uma seção ao documento (equivalente a uma página no PDF)
            var section = document.AddSection();

            // Define as configurações da página com base no setup padrão do documento
            section.PageSetup = document.DefaultPageSetup.Clone();

            // Define o formato da página para A4
            section.PageSetup.PageFormat = PageFormat.A4;

            // Define as margens: esquerda, direita, superior e inferior
            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.PageSetup.TopMargin = 80;
            section.PageSetup.BottomMargin = 80;

            // Retorna a seção criada
            return section;
        }

        private void CreateHeaderWithProfilePhotoAndName(Section page)
        {
            var table = page.AddTable();
            table.AddColumn();
            table.AddColumn("300");

            var row = table.AddRow();

            var assembly = Assembly.GetExecutingAssembly();
            var directoryName = Path.GetDirectoryName(assembly.Location);

            var pathFile = Path.Combine(directoryName!, "Logo", "ProfilePhoto.png");

            row.Cells[0].AddImage(pathFile);
            row.Cells[1].AddParagraph("Olá, Wagner");
            row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
            row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;

        }

        private byte[] RenderDocument(Document document)
        {
            var renderer =  new PdfDocumentRenderer
            {
                Document = document,
            };

            renderer.RenderDocument();
            using var file = new MemoryStream();
            renderer.PdfDocument.Save(file);
            return file.ToArray();
        }


        private void CreateTotalSpentSection(Section page , DateOnly date , decimal totalBills)
        {
            var paragraph = page.AddParagraph();
            paragraph.Format.SpaceBefore = "40";
            paragraph.Format.SpaceAfter = "40";


            var title = string.Format(ResourceReportMessages.TOTALGASTOEM, date.ToString("Y"));
            paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

            paragraph.AddLineBreak();
            

            paragraph.AddFormattedText($"{CURRECY_SYMBOL} {totalBills}", new Font { Name = FontHelper.RALEWAY_BLACK, Size = 50 });

        }


        private Table CreateBillTable(Section page)
        {
            var table = page.AddTable();
            var column = table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

            return table;
        }


        private void AddBillDescription(Cell cell, string Description)
        {
            cell.AddParagraph(Description);
            cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.RED_LIGHT;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.MergeRight = 2;
            cell.Format.LeftIndent = 15;
        }

        private void AddHeaderAmount(Cell cell)
        {
            cell.AddParagraph(ResourceReportMessages.VALOR);
            cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
            cell.Shading.Color = ColorsHelper.RED_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void SetStyleBaseForBillInformation(Cell cell)
        {
            cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.GREEN_DARK;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void AddAmountForBill(Cell cell , decimal amount)
        {
            cell.AddParagraph($"{CURRECY_SYMBOL} {Math.Round(amount, 2)} ");
            cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
            cell.Shading.Color = ColorsHelper.WHITE;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        private void AddWhiteSpace(Table table )
        {
            var row = table.AddRow();
            row.Height = 30;
            row.Borders.Visible = false;
        }


    }


    

   
}
