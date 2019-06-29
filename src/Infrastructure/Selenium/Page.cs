﻿using Optivem.Framework.Core.Common.WebAutomation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Optivem.Framework.Infrastructure.Selenium
{
    public class Page : PageObject
    {
        public Page(Driver driver, string url, bool navigateTo) 
            : base(driver)
        {
            if(navigateTo)
            {
                Driver.Url = url;
            }

            Url = url;

            if(!IsOpen())
            {
                throw new PageNotOpenException();
            }
        }

        public string Url { get; }

        public bool IsOpen()
        {
            return Driver.Url == Url;
        }
    }
}