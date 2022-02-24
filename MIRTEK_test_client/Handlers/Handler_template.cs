

namespace MIRTEK_test_client
{
    internal class Handler_template : IHandler
    {
        private IHandler _nextHandler;
        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }
        public virtual object Handle(byte[] byte_array_from_server, int start_position)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(byte_array_from_server, start_position);
            }
            else
            {
                return null;
            }
        }
    }
}
