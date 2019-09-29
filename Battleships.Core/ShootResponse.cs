using Battleships.Core.Grid;
using System;

namespace Battleships.Core
{
    public class ShootResponse
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }
        public string StatusDescription { get; private set; }
        public GridStates Status { get; private set; }

        public ShootResponse()
        {
            this.IsSuccess = true;
        }

        public void SetError(string error)
        {
            this.Error = error;
            this.IsSuccess = false;
        }

        public void SetStatus(GridStates status, string statusDescription=null)
        {
            this.Status = status;
            if(String.IsNullOrEmpty(statusDescription))
                this.StatusDescription = status.ToString();
            else
                this.StatusDescription = statusDescription;
        }
    }
}
