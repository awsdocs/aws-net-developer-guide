.. Copyright 2010-2016 Amazon.com, Inc. or its affiliates. All Rights Reserved.

   This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0
   International License (the "License"). You may not use this file except in compliance with the
   License. A copy of the License is located at http://creativecommons.org/licenses/by-nc-sa/4.0/.

   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
   either express or implied. See the License for the specific language governing permissions and
   limitations under the License.

.. _net-dg-nuget:

#################################
Install AWS Assemblies with NuGet
#################################

`NuGet <http://nuget.org/>`_ is a package management system for the .NET platform. With NuGet, you
can add the `AWSSDK assembly <http://nuget.org/packages/AWSSDK>`_ and the `TraceListener
<http://www.nuget.org/packages/AWS.TraceListener>`_ and `SessionProvider
<http://www.nuget.org/packages/AWS.SessionProvider>`_ extensions to your application without first
installing the SDK.

NuGet always has the most recent versions of the AWS .NET assemblies, and also enables you to
install previous versions. NuGet is aware of dependencies between assemblies and installs required
assemblies automatically. Assemblies that are installed with NuGet are stored with your solution
rather than in a central location such as :code:`Program Files`. This enables you to install
assembly versions specific to a given application without creating compatibility issues for other
applications.

For more information about NuGet, go to the `NuGet documentation <http://docs.nuget.org/>`_.


.. contents:: **Topics**
    :local:
    :depth: 1

.. _net-dg-nuget-install:

Installation
============

To use NuGet, install it from the `Visual Studio Gallery on MSDN
<http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c>`_. If you are
using Visual Studio 2010 or later, NuGet is installed automatically.

You can use NuGet either from :guilabel:`Solution Explorer` or from the :guilabel:`Package Manager
Console`.


.. _net-dg-nuget-soln-use:

NuGet from Solution Explorer
============================

To use NuGet from Solution Explorer, right-click on your project and select :guilabel:`Manage NuGet
Packages...` from the context menu.

From the :guilabel:`Manage NuGet Packages` dialog box, select :guilabel:`Online` in the left pane.
You can then search for the package that you want to install using the search box in the upper right
corner. The screenshot shows the :code:`AWS.Extensions` assembly package. Notice that NuGet is aware
that this package has a dependency on the :code:`AWSSDK` assembly package; NuGet will therefore
install the :code:`AWSSDK` package if it is not already installed.

.. figure:: images/nuget-install-vs-dlg.png
    :scale: 65


.. _net-dg-nuget-pkg-manager:

NuGet Package Manager Console
=============================

To use NuGet from the Package Manager Console within Visual Studio:

* Visual Studio 2010 |ndash| From the :guilabel:`Tools` menu, select :guilabel:`Library Package
  Manager`, and click :guilabel:`Package Manager Console`.

* Visual Studio 2012 |ndash| From the :guilabel:`Tools` menu, select :guilabel:`Nuget Package
  Manager`, and click :guilabel:`Package Manager Console`.

From the console, you can install the AWS assemblies using the *Install-Package* command. For
example, to install the |sdk-net| assembly, use the following command line:

.. code-block:: sh

    PM> Install-Package AWSSDK

To install an earlier version of a package, use the :code:`-Version` option and specify the desired
package version. For example, to install version 1.5.1.0 of the |sdk-net| assembly, use the
following command line:

.. code-block:: sh

    PM> Install-Package AWSSDK -Version 1.5.1.0

The NuGet website provides a page for every package that is available through NuGet such as the
`AWSSDK <http://nuget.org/packages/AWSSDK>`_ and `AWS.Extensions
<http://nuget.org/packages/AWS.Extensions>`_ assemblies. The page for each package includes a sample
command line for installing the package using the console. Each page also includes a list of the
previous versions of the package that are available through NuGet.

For more information on Package Manager Console commands, see `Package Manager Console Commands
(v1.3)
<http://nuget.codeplex.com/wikipage?title=Package%20Manager%20Console%20Command%20Reference%20%28v1.3%29>`_.



