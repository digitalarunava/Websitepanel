// Copyright (c) 2012, Outercurve Foundation.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
// - Redistributions of source code must  retain  the  above copyright notice, this
//   list of conditions and the following disclaimer.
//
// - Redistributions in binary form  must  reproduce the  above  copyright  notice,
//   this list of conditions  and  the  following  disclaimer in  the documentation
//   and/or other materials provided with the distribution.
//
// - Neither  the  name  of  the  Outercurve Foundation  nor   the   names  of  its
//   contributors may be used to endorse or  promote  products  derived  from  this
//   software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,  BUT  NOT  LIMITED TO, THE IMPLIED
// WARRANTIES  OF  MERCHANTABILITY   AND  FITNESS  FOR  A  PARTICULAR  PURPOSE  ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL,  SPECIAL,  EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO,  PROCUREMENT  OF  SUBSTITUTE  GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)  HOWEVER  CAUSED AND ON
// ANY  THEORY  OF  LIABILITY,  WHETHER  IN  CONTRACT,  STRICT  LIABILITY,  OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE)  ARISING  IN  ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System.Management;

namespace WebsitePanel.Setup
{
	/// <summary>
	/// Wmi helper class.
	/// </summary>
	internal sealed class WmiHelper
	{
		// namespace
		private string ns = null;
        // scope
		private ManagementScope scope = null;

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="ns">Namespace.</param>
		public WmiHelper(string ns)
		{
			this.ns = ns;
		}

		/// <summary>
		/// Executes specified query.
		/// </summary>
		/// <param name="query">Query to execute.</param>
		/// <returns>Resulting collection.</returns>
		internal ManagementObjectCollection ExecuteQuery(string query)
		{
			ObjectQuery objectQuery = new ObjectQuery(query);

			ManagementObjectSearcher searcher =
				new ManagementObjectSearcher(WmiScope, objectQuery);
			return searcher.Get();
		}

		/// <summary>
		/// Retreives ManagementClass class initialized to the given WMI path.
		/// </summary>
		/// <param name="path">A ManagementPath specifying which WMI class to bind to.</param>
		/// <returns>Instance of the ManagementClass class.</returns>
		internal ManagementClass GetClass(string path)
		{
			return new ManagementClass(WmiScope, new ManagementPath(path), null);
		}

		/// <summary>
		/// Retreives ManagementObject class bound to the specified WMI path.
		/// </summary>
		/// <param name="path">A ManagementPath that contains a path to a WMI object.</param>
		/// <returns>Instance of the ManagementObject class.</returns>
		internal ManagementObject GetObject(string path)
		{
			return new ManagementObject(WmiScope, new ManagementPath(path), null);
		}

		public ManagementScope WmiScope
		{
			get
			{
				if(scope == null)
				{
					ManagementPath path = new ManagementPath(ns);
					scope = new ManagementScope(path, new ConnectionOptions());
					scope.Connect();
				}
				return scope;
			}
		}
	}
}
