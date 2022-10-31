namespace GameCore
{
    public class RhythmCollectItemAttribute
    {
        public string collectItemKey { private set; get; }
        public string[] attributeTypes { private set; get; }
        public int baseScore { private set; get; }
        public int baseHpIncrease { private set; get; }

        public RhythmCollectItemAttribute(string _collectItemKey, string[] _attributeTypes, int _baseScore, int _baseHpIncrease)
        {
            collectItemKey = _collectItemKey;
            attributeTypes = _attributeTypes;
            baseScore = _baseScore;
            baseHpIncrease = _baseHpIncrease;
        }
    }
}