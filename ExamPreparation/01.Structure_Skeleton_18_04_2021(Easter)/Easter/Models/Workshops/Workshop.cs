using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public Workshop()
        {

        }
        public void Color(IEgg egg, IBunny bunny)
        {
            while (bunny.Energy > 0 && bunny.Dyes.Count > 0 && !egg.IsDone())
            {
                IDye dyeUsed = bunny.Dyes.First();

                bunny.Work();
                dyeUsed.Use();
                egg.GetColored();
                if (dyeUsed.IsFinished())
                {
                    bunny.Dyes.Remove(dyeUsed);
                }
            }

            //while (true)
            //{
            //    if (bunny.Energy == 0)
            //    {
            //        break;
            //    }

            //    if (bunny.Dyes.Count == 0)
            //    {
            //        break;
            //    }

            //    if (egg.IsDone())
            //    {
            //        break;
            //    }

            //    IDye dye = bunny.Dyes.First();
            //    dye.Use();
            //    if (dye.IsFinished())
            //    {
            //        bunny.Dyes.Remove(dye);
            //    }
            //    egg.GetColored();
            //    bunny.Work();
            //}
        }

    }
}

