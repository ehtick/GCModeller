﻿/*
 Copyright 2008-2011 Gephi
 Authors : Mathieu Jacomy <mathieu.jacomy@gmail.com>
 Website : http://www.gephi.org

 This file is part of Gephi.

 DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS HEADER.

 Copyright 2011 Gephi Consortium. All rights reserved.

 The contents of this file are subject to the terms of either the GNU
 General Public License Version 3 only ("GPL") or the Common
 Development and Distribution License("CDDL") (collectively, the
 "License"). You may not use this file except in compliance with the
 License. You can obtain a copy of the License at
 http://gephi.org/about/legal/license-notice/
 or /cddl-1.0.txt and /gpl-3.0.txt. See the License for the
 specific language governing permissions and limitations under the
 License.  When distributing the software, include this License Header
 Notice in each file and include the License files at
 /cddl-1.0.txt and /gpl-3.0.txt. If applicable, add the following below the
 License Header, with the fields enclosed by brackets [] replaced by
 your own identifying information:
 "Portions Copyrighted [year] [name of copyright owner]"

 If you wish your version of this file to be governed by only the CDDL
 or only the GPL Version 3, indicate your decision by adding
 "[Contributor] elects to include this software in this distribution
 under the [CDDL or GPL Version 3] license." If you do not indicate a
 single choice of license, a recipient has the option to distribute
 your version of this file under either the CDDL, the GPL Version 3 or
 to extend the choice of license to its licensees as provided above.
 However, if you add GPL Version 3 code and therefore, elected the GPL
 Version 3 license, then the option applies only if the new code is
 made subject to such option by the copyright holder.

 Contributor(s):

 Portions Copyrighted 2011 Gephi Consortium.
 */

namespace org.gephi.layout.plugin.noverlap
{
	using Layout = org.gephi.layout.spi.Layout;
	using LayoutBuilder = org.gephi.layout.spi.LayoutBuilder;
	using LayoutUI = org.gephi.layout.spi.LayoutUI;
	using NbBundle = org.openide.util.NbBundle;
	using ServiceProvider = org.openide.util.lookup.ServiceProvider;

	/// <summary>
	/// @author Mathieu Jacomy
	/// </summary>
	public class NoverlapLayoutBuilder : LayoutBuilder
	{

		private readonly NoverlapLayoutUI ui = new NoverlapLayoutUI();

		public override string Name
		{
			get
			{
				return NbBundle.getMessage(typeof(NoverlapLayoutBuilder), "name");
			}
		}

		public override LayoutUI UI
		{
			get
			{
				return ui;
			}
		}

		public override Layout buildLayout()
		{
			return new NoverlapLayout(this);
		}

		private class NoverlapLayoutUI : LayoutUI
		{

			public override string Description
			{
				get
				{
					return NbBundle.getMessage(typeof(NoverlapLayoutBuilder), "description");
				}
			}

			public override Icon Icon
			{
				get
				{
					return null;
				}
			}

			public override JPanel getSimplePanel(Layout layout)
			{
				return null;
			}

			public override int QualityRank
			{
				get
				{
					return -1;
				}
			}

			public override int SpeedRank
			{
				get
				{
					return -1;
				}
			}
		}
	}

}