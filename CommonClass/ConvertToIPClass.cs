namespace CommonClass
{
    public static class ConvertToIPClass
    {
        public static string ConvertToIP(string ip)
        {
            try
            {
                if (string.IsNullOrEmpty(ip)) return "";

                ip = ip.Trim();


                if (ip.Length == 12)
                {
                    ip = ip.Substring(0, 3).TrimStart('0')
                        + "."
                        + ip.Substring(3, 3).TrimStart('0')
                        + "."
                        + ip.Substring(6, 3).TrimStart('0')
                        + "."
                        + ip.Substring(9, 3).TrimStart('0');
                }
                else if (ip.Length > 12)
                {
                    if (ip.Contains('.'))
                    {
                        ip = ip.Split('.')[0].TrimStart('0')
                        + "."
                        + ip.Split('.')[1].TrimStart('0')
                        + "."
                        + ip.Split('.')[2].TrimStart('0')
                        + "."
                        + ip.Split('.')[3].TrimStart('0');
                    }
                    else
                        return "";
                }
                else
                    return "";

                return ip;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static string AddDots(string ip)
        {
            try
            {
                if (string.IsNullOrEmpty(ip)) return "";


                if (ip.Length == 12)
                {
                    ip = ip.Substring(0, 3)
                        + "."
                        + ip.Substring(3, 3)
                        + "."
                        + ip.Substring(6, 3)
                        + "."
                        + ip.Substring(9, 3);
                }
                else if (ip.Length > 12)
                {
                    if (!ip.Contains('.'))
                    {
                        ip = ip.Split('.')[0]
                        + "."
                        + ip.Split('.')[1]
                        + "."
                        + ip.Split('.')[2]
                        + "."
                        + ip.Split('.')[3];
                    }
                    else
                        return "";
                }
                else
                    return "";

                return ip;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
