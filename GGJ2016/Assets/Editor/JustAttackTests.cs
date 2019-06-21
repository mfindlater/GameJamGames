using NUnit.Framework;
using JustAttack;
using System.IO;
using System;

public class JustAttackTests {

    [Serializable]
    class SaveData
    {
        public string Name = "Ryuudo";
        public int Health = 100;
    }

    [Test]
    public void SaveTest()
    {
        var filename = "test.sav";
        var filepath = Path.Combine(Game.DefaultSaveFolder, filename);
        var data = new SaveData();
        Game.Save(data, filename);

        bool exists = (File.Exists(filepath));


        if (exists)
        {
            File.Delete(filepath);
        }

        Assert.IsTrue(exists);
    }

    [Test]
    public void SaveLoadTest()
    {
        var filename = "test.sav";
        var filepath = Path.Combine(Game.DefaultSaveFolder, filename);
        var data = new SaveData();
        data.Health = 50;
        Game.Save(data, filename);
        Assert.IsTrue((File.Exists(filepath)));

        data = Game.Load<SaveData>(filename);

        Assert.AreEqual(data.Health, 50);

    }

    [Test]
    public void LoadTest()
    {
        var filename = "test.sav";
        var data = Game.Load<SaveData>(filename);
        Assert.IsNull(data);
    }

}
