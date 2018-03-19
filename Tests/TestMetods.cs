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

            Assert.AreEqual(true, uDataBase.Add(user2));
            User user3 = new User(1, 1, "qwerty", "Петров Иван Иванович", "Главный конструктор");
            Assert.AreEqual(true, uDataBase.Add(user2));
        }

        [TestMethod]
        public void AddFileTest()
        {
            FileAttachments fa = new FileAttachments();
            Assert.AreEqual(true, fa.AddFile(new UserFile(100000000)));

            Assert.AreEqual(false, fa.AddFile(new UserFile(10000000)));


        }

        [TestMethod()]
        public void AddFilesTest()
        {
            FileAttachments fa = new FileAttachments();

            UserFile[] array = new UserFile[2];
            array[0] = new UserFile(100000000);
            array[1] = new UserFile(10000000);


            Assert.AreEqual(false, fa.AddFiles(array));

            array[1] = new UserFile(1000000);
            Assert.AreEqual(true, fa.AddFiles(array));

        }

        [TestMethod()]
        public void RemoveFileTest()
        {
            FileAttachments fa = new FileAttachments();
            fa.AddFile(new UserFile(1000));
            fa.AddFile(new UserFile(1001));
            fa.AddFile(new UserFile(1002));
            fa.AddFile(new UserFile(1003));
            fa.RemoveFile("test_1000.txt");
            Assert.AreEqual(3, fa.Count);
            fa.RemoveFile("test_1002.txt");
            Assert.AreEqual(2, fa.Count);
            fa.RemoveFile("test_1001.txt");
            Assert.AreEqual(1, fa.Count);
            fa.RemoveFile("test_1003.txt");
            Assert.AreEqual(0, fa.Count);


        }

        /* [TestMethod()]
         public void MessageUidTest()
         {
             Message m1 = new Message("test12123123123123123123123123123123123123123123123123123123213", 1, new int[] { 1 });
             Message m2 = new Message("test", 1, new int[] { 1 });

             Assert.AreNotEqual(m1.UidMsg, m2.UidMsg);

         }*/

        [TestMethod()]
        public void UserInfoConstructorTest()
        {
            UserInfo ui = new UserInfo(3, 3, "edfedfef", "eferfefr");
            Assert.AreEqual("", ui.Password);
        }
        [TestMethod()]
        public void EncryptDecryptTest()
        {
            string str = "test";
            Assert.AreEqual("test", Hash.DecodeString(Hash.EncodeString(str)));
        }


       

    }
}
