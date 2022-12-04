﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToolBox;

namespace ToolBoxTest
{
    [TestClass]
    public class TAesTest
    {
        [TestMethod]
        public void EncryptTest()
        {
            DateTime date1 = new DateTime(2022, 3, 14);
            DateTime date2 = new DateTime(2022, 11, 30);
            var days = (date2 - date1).TotalDays;

            //DateTime date1 = new DateTime(2015, 11, 30);
            //DateTime date2 = new DateTime(2022, 9, 22);
            //var days = (date2 - date1).TotalDays;

            string result = TAes.Encrypt("汉语", "hello");
            Assert.AreEqual(result, "lb0rbR1W+FBpAwWTTF8DIQ==");
        }

        [TestMethod]
        public void DecryptTest()
        {
            string result = TAes.Decrypt("lb0rbR1W+FBpAwWTTF8DIQ==", "hello");
            Assert.AreEqual(result, "汉语");
        }

        [TestMethod]
        public void LocalFileTest()
        {
            byte[] score = TStream.GetFileByteArr("d:\\1.docx");
            byte[] convert = TAes.Encrypt(score, TAes.GetAesKey("hello", 128), TAes.GetIv(TAes.defultIv), CipherMode.CFB, TAes.defaultPaddingMode);
            TStream.SaveFile(convert, "d:\\2.docx");
            byte[] result = TAes.Decrypt(convert, TAes.GetAesKey("hello", 128), TAes.GetIv(TAes.defultIv), CipherMode.CFB, TAes.defaultPaddingMode);
            TStream.SaveFile(result, "d:\\3.docx");
        }
    }
}