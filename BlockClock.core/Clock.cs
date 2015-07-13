using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockClock.core
{
    public class Clock
    {
        public int BlockSize { get; private set; }
        public int BPH { get; private set; }
        public int BPD { get; private set; }


        public Clock(int blockSize, bool bypass = false)
        {
            if (!new[] { 1, 2, 5, 10, 15, 20, 30 }.Contains(blockSize) && !bypass)
            {
                throw new ArgumentOutOfRangeException("blockSize");
            }
            BlockSize = blockSize;
            BPH = CalculateBlocksPerHour();
            BPD = CalculateBlocksPerDay(BPH);
        }

        public int Calculate()
        {
            return Calculate(DateTime.Now);
        }

        public int Calculate(DateTime date)
        {
            return Calculate(date.TimeOfDay);
        }

        public int Calculate(TimeSpan time)
        {

            int blocksPerHour = BPH;
            int blocksPerDay = BPD;
            int totalMinutes = Convert.ToInt32(time.TotalMinutes) - 1;
            int currentBlock = 1 + (totalMinutes / BlockSize);

            return currentBlock;
        }

        public int CalculateBlocksPerHour()
        {
            const int minPerHour = 60;

            return minPerHour / BlockSize;
        }

        public int CalculateBlocksPerDay(int blocksPerHour)
        {
            const int hoursPerDay = 24;
            return blocksPerHour * hoursPerDay;
        }

        public string WriteTable()
        {
            int bph = BPH;
            int bpd = BPD;
            int size = BlockSize;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bpd; i++)
            {
                TimeSpan @from, to;
                @from = TimeSpan.FromMinutes(i * size);
                to = TimeSpan.FromMinutes(((i + 1) * size) - 1);
                sb.AppendFormat("block {0} from {1} to {2}", i + 1, @from, to).AppendLine();
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            int i = Calculate();
            int size = BlockSize;
            TimeSpan @from, to;
            @from = TimeSpan.FromMinutes(i * size);
            to = TimeSpan.FromMinutes(((i + 1) * size) - 1);
            return string.Format("current block: {0}/{1} from {2} to {3}", i, BPD, @from, to);
        }
    }
}
