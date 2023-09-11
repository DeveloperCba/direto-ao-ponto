using System.Text.RegularExpressions;

namespace ConsoleNoSql.Helpers
{
    public static class StringExtension
    {
        /// <summary>
        /// Método responsável por remover as letras do texto.
        /// </summary>
        /// <param name="text">Infomre o texto.</param>
        /// <returns>Retorna somente número.</returns>
        public static string ReturnNumberOnly(this string text)
        {
            return Regex.Replace(text, "[^0-9,]", "");
        }
    }
}