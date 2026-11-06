using System.Text;

namespace Atbas;

public static class Rejtjelek
{
    public static bool Nagybetu(char c) => c >= 'A' && c <= 'Z';
    public static bool Kisbetu(char c) => c >= 'a' && c <= 'z';
    public static bool Betu(char c) => Nagybetu(c) || Kisbetu(c);

    public static string Atbas(string szoveg)
    {
        StringBuilder sb = new StringBuilder(szoveg.Length);
        foreach (char c in szoveg)
        {
            if (Nagybetu(c)) sb.Append((char)('Z' - (c - 'A')));
            else if (Kisbetu(c)) sb.Append((char)('z' - (c - 'a')));
            else sb.Append(c);
        }
        return sb.ToString();
    }

    public static string Rot13(string szoveg)
    {
        StringBuilder sb = new StringBuilder(szoveg.Length);
        foreach (char c in szoveg)
        {
            if (Nagybetu(c)) sb.Append((char)('A' + (c - 'A' + 13) % 26));
            else if (Kisbetu(c)) sb.Append((char)('a' + (c - 'a' + 13) % 26));
            else sb.Append(c);
        }
        return sb.ToString();
    }
}
