namespace Library.DAL.DataManager
{
    public class SqlUserAccess
    {

        public static bool LiveServer = false;

        //// Runing Serve

        //public static string DataSource = @"192.168.110.50\sa";
        //public static string UserId = @"sa";
        //public static string PassWord = @"sa1234";


        //public static string DataSource = @"45.64.134.85\MSSQLSERVER2014";
        //public static string UserName = @"sa";
        //public static string PassWord = @"sa1234#";

        // Publish

        //public static string DataSource = @"103.244.247.93\MSSQLSERVER2019";
        //public static string UserId = @"sa";
        //public static string Password = @"*Sa1234#";

        //public static string DataSource = @"TOWSIF-PC\MSSQLSERVER2019";
        //public static string UserId = @"sa";
        //public static string Password = @"sa1234";


        //public static string DataSource = @"TOWSIF-PC\MSSQLSERVER2019";
        //public static string UserId = @"sa";
        //public static string PassWord = @"sa1234";

        public static string DataSource = @"CSTL-VM-01\MSSQLSERVER2019";
        public static string UserId = @"sa";
        public static string PassWord = @"N@fcoZ@s2023#";

        // New Live

        //public static string DataSource = @"103.244.247.93\MSSQLSERVER2019";
        //public static string UserName = @"sa";
        //public static string PassWord = @"*Sa1234#";


        // local

        //public static string DataSource = @"103.117.194.123\MSSQLSERVER2019";
        //public static string UserName = "sa";
        //public static string PassWord = "Sa1234!@#$";

        //public static string DataSource = @"192.168.110.50\sa";
        //public static string UserName = "sa";
        //public static string PassWord = "Sa1234";


        ////////////////////////// Rest API calling
        /////////////////////////
        public static string AppName = "CSTL-Development";
        public static string BASE_URL = "http://45.64.134.85:570";


    }
}