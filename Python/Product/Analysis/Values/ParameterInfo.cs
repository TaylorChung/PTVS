﻿// Python Tools for Visual Studio
// Copyright(c) Microsoft Corporation
// All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the License); you may not use
// this file except in compliance with the License. You may obtain a copy of the
// License at http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS
// OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY
// IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
// MERCHANTABLITY OR NON-INFRINGEMENT.
//
// See the Apache Version 2.0 License for specific language governing
// permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    class ParameterInfo : LazyValueInfo {
        private readonly FunctionInfo _function;

        public ParameterInfo(FunctionInfo function, Node node, string name) : base(node) {
            _function = function;
            Name = name;
        }

        public override string Name { get; }

        public override IPythonProjectEntry DeclaringModule => _function.DeclaringModule;
        public override int DeclaringVersion => _function.DeclaringVersion;

        public override IAnalysisSet Resolve(AnalysisUnit unit, FunctionInfo calling, ArgumentSet callingArgs) {
            if (_function == calling) {
                return _function.ResolveParameter(unit, Name, callingArgs);
            }
            return _function.ResolveParameter(unit, Name);
        }

        public override string ToString() => $"<arg {Name} in {_function.Name}>";
    }
}
