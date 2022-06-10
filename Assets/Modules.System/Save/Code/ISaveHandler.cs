namespace Game.Save
{
    public interface ISaveHandler
    {
        bool Dirty { get; set; }

        void SaveData();
    }
}
