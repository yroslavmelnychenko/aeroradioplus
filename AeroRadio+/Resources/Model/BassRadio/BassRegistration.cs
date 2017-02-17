using System;
using System.Runtime.InteropServices;
using Un4seen.Bass;

namespace AeroRadio_.Resources.Model.BassRadio
{
   public class BassRegistration
    {
        public BassRegistration(string email , string password)
        {
            BassNet.Registration(email, password);
        }
    }
}
