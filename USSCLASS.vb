using System;
using System.IO.Ports;

// Author: Mostafa Soghandi
// Email: Mostafa.soghandi@gmail.com

namespace Uss
{
    public class UssModule
    {
        public event EventHandler<string> ReceiveDataHandler;

        private void OnReceiveData(object sender, string data)
        {
            EventHandler<string> handler = ReceiveDataHandler;
            handler?.Invoke(sender, data);
        }

        private readonly SerialPort _msComm1 = new SerialPort("COM1", 19200, Parity.Even, 8, StopBits.One);

        public void PortOpen()
        {
            if (_msComm1.IsOpen) return;
            _msComm1.ReadBufferSize = 512;
            _msComm1.WriteBufferSize = 512;
            _msComm1.ReceivedBytesThreshold = 15;
            _msComm1.DtrEnable = true;
            _msComm1.RtsEnable = true;
            _msComm1.DataReceived += RecieveData;
            _msComm1.Open();
        }

        private void RecieveData(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
        {
            var inByte = new byte[128];
            string buf = "";
            _msComm1.Read(inByte, 0, 127);
            int jm = Convert.ToInt32(inByte[1].ToString());
            var loopTo = jm - 1;
            for (int i = 0; i <= loopTo; i++)
            {
                buf = buf + (inByte[i] > 15 ? inByte[i].ToString("X") : "0" + inByte[i].ToString("X")) + ' ';
            }

            buf = Convert.ToString(buf);
            string rcvStr = jm + ":" + buf;
            OnReceiveData(this, rcvStr);
        }

        public void PortClose()
        {
            _msComm1.Close();
        }

        public void RunMms(int freq, int address)
        {
            var i = new byte[16];
            string pinH = "";
            string pinL = "";
            var pin = (float) (freq * 16384 / 50d);
            if (pin.ToString("X").Length == 4)
            {
                pinH = pin.ToString("X").Substring(1, 2);
                pinL = pin.ToString("X").Substring(3, 2);
            }

            if (pin.ToString("X").Length < 4)
            {
                pinH = pin.ToString("X").Substring(1, 1);
                pinL = pin.ToString("X").Substring(2, 2);
            }

            i[0] = 0x2;
            i[1] = 0xE;
            i[2] = Convert.ToByte("&H" + address.ToString("X"));
            i[3] = 0x0;
            i[4] = 0x0;
            i[5] = 0x0;
            i[6] = 0x0;
            i[7] = 0x0;
            i[8] = 0x0;
            i[9] = 0x0;
            i[10] = 0x0;
            i[11] = 0x4;
            i[12] = 0x7F;
            i[13] = Convert.ToByte("&H" + pinH);
            i[14] = Convert.ToByte("&H" + pinL);
            for (int j = 0; j <= 14; j++)
                i[15] = (byte) (i[15] ^ i[j]);
            _msComm1.Write(i, 0, i.Length);
        }

        public void StopRunning(int address)
        {
            var i = new byte[16];
            int j;
            i[0] = 0x2;
            i[1] = 0xE;
            i[2] = Convert.ToByte("&H" + address.ToString("X"));
            i[3] = 0x0;
            i[4] = 0x0;
            i[5] = 0x0;
            i[6] = 0x0;
            i[7] = 0x0;
            i[8] = 0x0;
            i[9] = 0x0;
            i[10] = 0x0;
            i[11] = 0x4;
            i[12] = 0x7E;
            i[13] = 0x0;
            i[14] = 0x0;
            for (j = 0; j <= 14; j++)
                i[15] = (byte) (i[15] ^ i[j]);
            _msComm1.Write(i, 0, i.Length);
        }

        public bool ReverseRun(int freq, int address)
        {
            var i = new byte[16];
            int j;
            string pinH = "";
            string pinL = "";
            var pin = (float) (freq * 16384d / 50d);
            if (pin.ToString("X").Length == 4)
            {
                pinH = pin.ToString("X").Substring(1, 2);
                pinL = pin.ToString("X").Substring(3, 2);
            }

            if (pin.ToString("X").Length < 4)
            {
                pinH = pin.ToString("X").Substring(1, 1);
                pinL = pin.ToString("X").Substring(2, 2);
            }

            i[0] = 0x2;
            i[1] = 0xE;
            i[2] = Convert.ToByte("&H" + address.ToString("X"));
            i[3] = 0x0;
            i[4] = 0x0;
            i[5] = 0x0;
            i[6] = 0x0;
            i[7] = 0x0;
            i[8] = 0x0;
            i[9] = 0x0;
            i[10] = 0x0;
            i[11] = 0xC;
            i[12] = 0x7F;
            i[13] = Convert.ToByte("&H" + pinH);
            i[14] = Convert.ToByte("&H" + pinL);
            for (j = 0; j <= 14; j++)
                i[15] = (byte) (i[15] ^ i[j]);
            _msComm1.Write(i, 0, i.Length);
            return default;
        }

        public bool RunMMSOnJOGMode(int address)
        {
            var i = new byte[16];
            int j;
            i[0] = 0x2;
            i[1] = 0xE;
            i[2] = Convert.ToByte("&H" + address.ToString("X"));
            i[3] = 0x0;
            i[4] = 0x0;
            i[5] = 0x0;
            i[6] = 0x0;
            i[7] = 0x0;
            i[8] = 0x0;
            i[9] = 0x0;
            i[10] = 0x0;
            i[11] = 0x5;
            i[12] = 0x7E;
            i[13] = 0x0;
            i[14] = 0x0;
            for (j = 0; j <= 14; j++)
                i[15] = (byte) (i[15] ^ i[j]);
            _msComm1.Write(i, 0, i.Length);
            return default;
        }

        public bool ReverseJog(int address)
        {
            var i = new byte[16];
            int j;
            i[0] = 0x2;
            i[1] = 0xE;
            i[2] = Convert.ToByte("&H" + address.ToString("X"));
            i[3] = 0x0;
            i[4] = 0x0;
            i[5] = 0x0;
            i[6] = 0x0;
            i[7] = 0x0;
            i[8] = 0x0;
            i[9] = 0x0;
            i[10] = 0x0;
            i[11] = 0x6;
            i[12] = 0x7E;
            i[13] = 0x0;
            i[14] = 0x0;
            for (j = 0; j <= 14; j++)
                i[15] = (byte) (i[15] ^ i[j]);
            _msComm1.Write(i, 0, i.Length);
            return default;
        }

        public bool StopJog(int address)
        {
            var i = new byte[16];
            int j;
            i[0] = 0x2;
            i[1] = 0xE;
            i[2] = Convert.ToByte("&H" + address.ToString("X"));
            i[3] = 0x0;
            i[4] = 0x0;
            i[5] = 0x0;
            i[6] = 0x0;
            i[7] = 0x0;
            i[8] = 0x0;
            i[9] = 0x0;
            i[10] = 0x0;
            i[11] = 0x4;
            i[12] = 0x7E;
            i[13] = 0x0;
            i[14] = 0x0;
            for (j = 0; j <= 14; j++)
                i[15] = (byte) (i[15] ^ i[j]);
            _msComm1.Write(i, 0, i.Length);
            return default;
        }

        public bool ReqParam(int address)
        {
            var i = new byte[16];
            int j;
            i[0] = 0x2;
            i[1] = 0xE;
            i[2] = Convert.ToByte("&H" + address.ToString("X"));
            i[3] = 0x10;
            i[4] = 0x3;
            i[5] = 0x0;
            i[6] = 0x0;
            i[7] = 0x0;
            i[8] = 0x3;
            i[9] = 0x0;
            i[10] = 0x0;
            i[11] = 0x0;
            i[12] = 0x0;
            i[13] = 0x0;
            i[14] = 0x0;
            for (j = 0; j <= 14; j++)
                i[15] = (byte) (i[15] ^ i[j]);
            _msComm1.Write(i, 0, i.Length);
            return default;
        }
    }
}
