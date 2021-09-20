using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Book.UnitTests.ControllerTests
{
    public static class ControllerTestExtensions
    {
        public static T WithAnonymousIdentity<T>(this T controller) where T : ControllerBase
        {
            controller.EnsureHttpContext();

            return controller;
        }

        private static T EnsureHttpContext<T>(this T controller) where T : ControllerBase
        {
            if (controller.ControllerContext == null)
            {
                controller.ControllerContext = new ControllerContext();
            }

            if (controller.ControllerContext.HttpContext == null)
            {
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
            }

            return controller;
        }
    }
}
