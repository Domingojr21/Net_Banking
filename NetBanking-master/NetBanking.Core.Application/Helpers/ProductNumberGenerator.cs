using NetBanking.Core.Domain.Common;

namespace NetBanking.Core.Application.Helpers
{
    public static class ProductNumberGenerator
    {
        

        public static string AlgorithmForProductNumber<T>(string prefix, List<T> products) where T : Product
        {
            Random random = new Random();
            HashSet<string> existingNumbers = new HashSet<string>(products.Select(product => product.IdentifyingNumber));

            if (existingNumbers.Count >= 9000)
            {
                throw new InvalidOperationException("Todos los números posibles con este prefijo están ocupados. Hay que Cambiar el prefijo.");
            }

            string fullNumber;
            string randomNumber = random.Next(100000, 999999).ToString("d4");
            fullNumber = $"{prefix}{randomNumber}";

            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    if (!existingNumbers.Contains(fullNumber))
                    {
                        return fullNumber;
                    }
                }
            }else
            {
                return fullNumber;
            }


            throw new InvalidOperationException("No se pudo generar un número único. Hubo un Problema");

        }


    }
}
