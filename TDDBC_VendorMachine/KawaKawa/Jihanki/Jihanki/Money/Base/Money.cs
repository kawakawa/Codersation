﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Money.Base
{
    public class Money
    {
        /// <summary>
        /// 金額
        /// </summary>
        private int _money=0;

        /// <summary>
        /// 数
        /// </summary>
        private int num = 0;


        public Money(int money)
        {
            this._money = money;
        }



        public void Add(int num)
        {
            this.num = this.num + num;
        }


        /// <summary>
        /// 合計金額
        /// </summary>
        /// <returns></returns>
        public int Sum()
        {
            return this._money*this.num;
        }


    }
}
