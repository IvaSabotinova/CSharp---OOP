using System.Collections.Generic;

namespace Book.Tests
{
    using System;

    using NUnit.Framework;
    [TestFixture]
    public class Tests
    {
        private Book book;
       
        [SetUp]
        public void SetUp()
        {
            book = new Book("aaa", "author");
            book.AddFootnote(1, "aaa");
            
        }

        [Test]
        [TestCase(null, "author")]
        [TestCase("", "author1")]
        [TestCase("book", null)]
        [TestCase("book1", "")]
        public void CheckCtor(string bookName, string author)
        {
            Assert.Throws<ArgumentException>((() => book = new Book(bookName, author)));
            Assert.AreEqual("aaa", book.BookName);
            Assert.AreEqual("author", book.Author);
        }
       
        [Test]
        public void CheckDictionaryCount()
        {
            book.AddFootnote(3, "ccc");
            book.AddFootnote(2, "bbb");
            Assert.AreEqual(3, book.FootnoteCount);
        }
        [Test]
        public void Exception_AddingFootnoteWhichExists()
        {
            Assert.Throws<InvalidOperationException>(() => book.AddFootnote(1, "bbb"), "Footnote already exists!");
        }

        [Test]
        public void Exception_CheckMethodFindFootnoteThatDoesNotEXist()
        {
            Assert.Throws<InvalidOperationException>((() => book.FindFootnote(5)), "Footnote doesn't exists!");
        }

        [Test]
        public void CheckMethodFindFootnoteThatExists()
        {
            Assert.AreEqual($"Footnote #{1}: {"aaa"}", book.FindFootnote(1));
        }

        [Test]
        public void Exception_AlterFootNoteThatDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>((() => book.AlterFootnote(5, "newText")), "Footnote does not exists!");

        }

        [Test]
        public void CheckMethodAlterFootnote()
        {
            book.AlterFootnote(1, "newText");
            Assert.AreEqual($"Footnote #{1}: {"newText"}", book.FindFootnote(1));
        }
    }
}