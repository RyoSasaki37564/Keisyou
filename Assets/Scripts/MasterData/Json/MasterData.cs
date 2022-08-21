using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData
{
    [Serializable]
    public class Card
    {
        public int Id;
        public string Name;
        public int Cost;
        public int Power;
        public int Toughness;
    }

    [Serializable]
    public class Effect
    {
        public int Id;
        public int CardId;
        public string Text;
    }

    [Serializable]
    public class Cube
    {
        public int Id;
        public string Text;
        public string Text1;
        public int CardId1;
        public int CardId2;
        public int CardId3;
    }

    [Serializable]
    public class MasterDataClass<T>
    {
        public string Version;
        public T[] Data;
    }
}