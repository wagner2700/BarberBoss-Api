using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Reports;

namespace BarberBoss.Domain.Extensions
{
    public static class PaymentTypeExtensions
    {
        public static string PaymentTypeToString(this PaymentType paymentType)
        {
            return paymentType switch
            {
                PaymentType.Dinheiro => ResourceReportMessages.DINHEIRO,
                PaymentType.CartaoCredito => ResourceReportMessages.CARTÃOCREDITO,
                PaymentType.Pix => ResourceReportMessages.PIX,
                PaymentType.CartaoDebito => ResourceReportMessages.CARTAODEBITO,
                _ => string.Empty,
            };
        }
    }
}
