// Copyright 2007-2015 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace GreenPipes.Filters
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;


    public class DelegateFilter<T> :
        IFilter<T>
        where T : class, PipeContext
    {
        readonly Action<T> _callback;

        public DelegateFilter(Action<T> callback)
        {
            _callback = callback;
        }

        void IProbeSite.Probe(ProbeContext context)
        {
            context.CreateFilterScope("delegate");
        }

        [DebuggerNonUserCode]
        [DebuggerStepThrough]
        public Task Send(T context, IPipe<T> next)
        {
            _callback(context);

            return next.Send(context);
        }
    }
}