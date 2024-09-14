﻿using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Reports.Fonts
{
    public class BillReportFontResolver : IFontResolver
    {
        public byte[]? GetFont(string faceName)
        {
            var stream = ReadFontFile(faceName);

            if(stream is null)
            {
                stream = ReadFontFile(FontHelper.DEFAULT_FONT);
            }

            var lenght = (int)stream!.Length;

            var data = new byte[lenght];

            stream.Read(buffer: data , offset: 0 , count: lenght);

            return data;
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
        {
            return new FontResolverInfo(familyName);
        }

        private Stream? ReadFontFile(string faceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return assembly.GetManifestResourceStream($"BarberBoss.Application.UseCases.Reports.Fonts.{faceName}.ttf");
        }
    }
}
