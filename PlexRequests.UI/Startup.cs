﻿#region Copyright
// /************************************************************************
//    Copyright (c) 2016 Jamie Rees
//    File: Startup.cs
//    Created By: Jamie Rees
//   
//    Permission is hereby granted, free of charge, to any person obtaining
//    a copy of this software and associated documentation files (the
//    "Software"), to deal in the Software without restriction, including
//    without limitation the rights to use, copy, modify, merge, publish,
//    distribute, sublicense, and/or sell copies of the Software, and to
//    permit persons to whom the Software is furnished to do so, subject to
//    the following conditions:
//   
//    The above copyright notice and this permission notice shall be
//    included in all copies or substantial portions of the Software.
//   
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ************************************************************************/
#endregion
using System;
using System.Diagnostics;
using Ninject;
using Ninject.Planning.Bindings.Resolvers;

using NLog;

using Owin;

using PlexRequests.UI.Helpers;
using PlexRequests.UI.Jobs;
using PlexRequests.UI.NinjectModules;

namespace PlexRequests.UI
{
    public class Startup
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public void Configuration(IAppBuilder app)
        {
            try
            {
              Debug.WriteLine("Starting StartupConfiguration");
                var resolver = new DependancyResolver();

              Debug.WriteLine("Created DI Resolver");
                var modules = resolver.GetModules();
              Debug.WriteLine("Getting all the modules");


              Debug.WriteLine("Modules found finished.");
                var kernel = new StandardKernel(modules);
              Debug.WriteLine("Created Kernel and Injected Modules");

              Debug.WriteLine("Added Contravariant Binder");
                kernel.Components.Add<IBindingResolver, ContravariantBindingResolver>();

              Debug.WriteLine("Start the bootstrapper with the Kernel.ı");
               app.UseNancy(options => options.Bootstrapper = new Bootstrapper(kernel));
              Debug.WriteLine("Finished bootstrapper");
                var scheduler = new Scheduler();
                scheduler.StartScheduler();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception);
                throw;
            }
        }
    }
}