﻿namespace Productions.UI.Cultivations
{
    public class CultivationUrl
    {
        public const string Prefix = "api/cultivation";

        public const string gets = "gets";
        public static string ApiGets => $"{Prefix}/{gets}";

        public const string get = "get";
        public static string ApiGet => $"{Prefix}/{get}";

        public const string insert = "insert";
        public static string ApiInsert => $"{Prefix}/{insert}";

        public const string update = "update";
        public static string ApiUpdate => $"{Prefix}/{update}";

        public const string delete = "delete";
        public static string ApiDelete => $"{Prefix}/{delete}";
    }
}
