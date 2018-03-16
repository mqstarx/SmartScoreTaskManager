using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoreLib;
namespace Tests
{
    [TestClass]
    public class TestMetods
    {
        [TestMethod]
        public void AddUserToDataBaseIdwhenIdIsContain()
        {
            UsersDataBase uDataBase = new UsersDataBase();
            User user1 = new User(1, -1, "qwerty", "Иванов Иван Иванович", "Генеральный директор");
            uDataBase.Add(user1);
            User user2 = new User(1, 1, "qwerty", "Петров Иван Иванович", "Главный инженер");
            Assert.AreEqual(false, uDataBase.Add(user2));
                      
        }
    }
}
