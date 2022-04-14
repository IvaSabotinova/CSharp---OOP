// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 




namespace FestivalManager.Tests
{
    using System.Linq;
    using FestivalManager.Entities;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class StageTests
    {
        private Song song;
        private Performer performer;
        private Stage stage;
       
        [SetUp]
        public void SetUp()
        {

            song = new Song("aaa", new TimeSpan(0, 2, 50));
            performer = new Performer("Pesho", "Goshev", 21);
            stage = new Stage();

        }
        [Test]
        public void CheckCollectionPerformersExists()
        {
            Assert.IsNotNull(stage.Performers);
           
        }

        [Test]
       
        public void CheckValidationOfPerformers_Songs()
        {
            performer = null;
            song = null;
            Assert.Throws<ArgumentNullException>(() => stage.AddPerformer(performer), "Can not be null!");
            Assert.Throws<ArgumentNullException>(() => stage.AddSong(song), "Can not be null!");
        }
        [Test]
        public void Exception_CheckAddPerformerMethod()
        {
            performer = new Performer("pesho", "peshov", 12);
            Assert.Throws<ArgumentException>(() => stage.AddPerformer(performer), "You can only add performers that are at least 18.");
        }
        [Test]
        public void CheckAddPerformerMethod()
        {

            stage.AddPerformer(performer);
            Performer performerNext = new Performer("Gosho", "Goshev", 27);
            stage.AddPerformer(performerNext);
            Assert.AreEqual(2, stage.Performers.Count);
        }
        [Test]
        public void Exception_CheckMethodAddSong()
        {
            song = new Song("aaa", new TimeSpan(0, 0, 25));
            Assert.Throws<ArgumentException>(() => stage.AddSong(song), "You can only add songs that are longer than 1 minute.");
        }
   
        [Test]
        public void Exception_CheckMethodGetSongThroughMethodAddSongToPerformer()
        {
           
            stage.AddPerformer(performer);
            stage.AddSong(song);
            performer.SongList.Add(song);
            Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("ddd", performer.FullName), "There is no song with this name.");
        }
        [Test]
        public void Exception_CheckGetPerformerMethodThroughMethodAddSongToPerformer()
        {

            stage.AddPerformer(performer);
            stage.AddSong(song);
            performer.SongList.Add(song);
            Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("aaa", "Misho Mishev"), "There is no performer with this name.");
        }

        [Test]
        public void CheckMethodAddSongToPerformer()
        {

            stage.AddPerformer(performer);
            stage.AddSong(song);
            performer.SongList.Add(song);
            Assert.AreEqual(1, performer.SongList.Count);
            Assert.AreEqual($"{song} will be performed by {performer}", stage.AddSongToPerformer("aaa", "Pesho Goshev"));
        }

        [Test]
        public void CheckMethodPlay()
        {

            Song song1 = new Song("aaa", new TimeSpan(0, 2, 25));
            Song song2 = new Song("bbb", new TimeSpan(0, 1, 25));
            Song song3 = new Song("bca", new TimeSpan(0, 1, 50));
            Performer performerNext = new Performer("Gosho", "Goshev", 27);
            stage.AddPerformer(performer);
            stage.AddPerformer(performerNext);
            stage.AddSong(song1);
            stage.AddSong(song2);
            stage.AddSong(song3);
            stage.AddSongToPerformer("aaa", "Pesho Goshev");
            stage.AddSongToPerformer("bbb", "Pesho Goshev");
            stage.AddSongToPerformer("bca", "Gosho Goshev");
            Assert.AreEqual($"2 performers played 3 songs", stage.Play());
        }


    }
}