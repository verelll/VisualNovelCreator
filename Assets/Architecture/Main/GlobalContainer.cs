namespace Architecture.IOC
{
    public static class GlobalContainer 
    {
        private static Container _main;

        public static Container Main
        {
            get
            {
                if (_main == null)
                {
                    _main = new Container();
                }
                return _main;
            }
        }
    }
}
