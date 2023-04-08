using System;
using Core.Contracts;

namespace Services.Classes
{
	public static class EmailTools
	{
        public static string GetAddressByEmail(string email) => email.Split("@")[0].ToString();
    }
}

