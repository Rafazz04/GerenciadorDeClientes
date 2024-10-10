using System.Text.RegularExpressions;

namespace GerenciadorDeClientes.Communication.Utils;

public static class Util
{
	public static string LimpaCnpj(string cnpj) => new string(cnpj.Where(char.IsDigit).ToArray());
	public static string LimpaCep(string cep) => new string(cep.Where(char.IsDigit).ToArray());

	public static string RemoverEspacosIgnoreCase(string input) => input != null ? Regex.Replace(input.Trim(), @"\s+", string.Empty, RegexOptions.IgnoreCase) : null;

}
