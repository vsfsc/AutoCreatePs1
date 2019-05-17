using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AutoCreatePs1
{
    class WorksEvaluation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="worksInfo">作品及作者信息</param>
        /// <param name="k">m个作品，m>k,每个人分k个作品，每个作品被k个人评</param>
        public static ArrayList WorksDistribution(int m, int k)
        {
            ArrayList allWorks = new ArrayList();
            Dictionary<int, int> pCounts = new System.Collections.Generic.Dictionary<int, int>();//每个人分配的作品个数
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            for (int w = 1; w < m + 1; w++)//遍历每个作品
            {
                ArrayList works = new ArrayList();
                works.Add(w);
                int c = 0;
                while (works.Count < k + 1)
                {
                    int p = ran.Next(1, m+1);
                    c = c + 1; ;
                    if (p != w)
                    {
                        if (!pCounts.ContainsKey(p))
                            pCounts.Add(p, 1);
                        if (pCounts[p] <= k)
                        {
                            if (!works.Contains(p))
                            {
                                pCounts[p] = pCounts[p] + 1;
                                works.Add(p);
                            }
                        }

                    }
                }
                if (works.Count == k + 1)
                    allWorks.Add(works);
            }
            return allWorks;
        }
    }
}
