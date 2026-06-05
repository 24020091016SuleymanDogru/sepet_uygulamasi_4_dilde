using MySql.Data.MySqlClient;

public class Veritabani
{
    // Kendi MySQL şifreni "pwd=" kısmına yazmayı unutma
    private static string baglantiYolu = "Server=localhost;Database=sepet_db;Uid=root;Pwd=suleyman;";

    public static MySqlConnection BaglantiAl()
    {
        return new MySqlConnection(baglantiYolu);
    }
}