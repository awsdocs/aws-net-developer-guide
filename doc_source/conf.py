# -*- coding: utf-8 -*-
#
# AWS Sphinx configuration file.
#
# For more information about how to configure this file, see:
#
# https://w.amazon.com/index.php/AWSDevDocs/Sphinx
#

#
# General information about the project.
#

# Optional service/SDK name, typically the three letter acronym (TLA) that
# represents the service, such as 'SWF'. If this is an SDK, you can use 'SDK'
# here.
service_name = "SDK"

# The long version of the service or SDK name, such as "Amazon Simple Workflow
# Service", "AWS Flow Framework for Ruby" or "AWS SDK for Java"
service_name_long = u'AWS SDK for .NET'

# The landing page for the service documentation.
service_docs_home = u'http://aws.amazon.com/documentation/sdk-for-net/'

# The project type, such as "Developer Guide", "API Reference", "User Guide",
# or whatever.
project = u'Developer Guide'

# A short description of the project.
project_desc = "AWS SDK for .NET Developer Guide"

# the output will be generated in V3/<project_basename> and
# its URL will feature the same basename.
project_basename = 'DeveloperGuide'

# This name is used as the manual / PDF name. Don't include the extension
# (.pdf) here.
man_name = 'aws-sdk-net-v3-dg'

# The language for this version of the docs. Typically 'en'. For a full list of
# values, see: http://sphinx-doc.org/config.html#confval-language
language = u'en'

# Whether or not to show the PDF link. If you generate a PDF for your
# documentation, set this to True.
show_pdf_link = True

#
# Version Information
#

# The version info for the project you're documenting, acts as replacement for
# |version| and |release| substitutions in the documentation, and is also used
# in various other places throughout the built documents.
#
# The short X.Y version.

version = '1.0'

# The full version, including alpha/beta/rc tags.

release = '1.0'


#
# Forum Information
#

# Optional forum ID. If there's a relevant forum at forums.aws.amazon.com, then
# set the ID here. If not set, then no forum ID link will be generated.
#

forum_id = '61'


#
# Extra Navlinks
#

# Extra navlinks. You can specify additional links to appear in the top bar here
# as navlink name / url pairs. If extra_navlinks is not set, then no extra
# navlinks will be generated.
#
# extra_navlinks = [
#         ('API Reference', 'http://docs.aws.amazon.com/sdkfornet/v3/apidocs/'),
#         ('GitHub', 'http://path/to/github/project'),
#         ]

extra_navlinks = []

# The link to the top of the doc source tree on GitHub. This allows generation
# of per-page "Edit on GitHub" links.
github_doc_url = 'http://github.com/awsdocs/aws-net-developer-guide/tree/master/doc_source'

#
# EXTRA_CONF_CONTENT -- don't change, move or remove this line!
#
# Any settings *below* this act as overrides for the default config content.
# Declare extlinks <http://sphinx-doc.org/latest/ext/extlinks.html> and
# additional configuration details specific to this documentation set here.
#

aws_docs_url = 'http://docs.aws.amazon.com/'
aws_url = 'http://aws.amazon.com/'
aws_blogs = 'http://blogs.aws.amazon.com/'
aws_blogs_net = aws_blogs + 'net/post/'

if 'extlinks' not in vars():
    extlinks = {}

#submitted
extlinks['twp-ug']       = (aws_docs_url + 'powershell/latest/userguide/%s.html', '')
extlinks['r53-dg']       = (aws_docs_url + 'Route53/latest/DeveloperGuide/%s.html', '')
extlinks['r53-api']      = (aws_docs_url + 'Route53/latest/APIReference/API_%s.html', '')
extlinks['s3-dg']        = (aws_docs_url + 'AmazonS3/latest/dev/%s.html', '')

#unsubmitted
extlinks['r53-dg-deep']  = (aws_docs_url + 'Route53/latest/DeveloperGuide/%s', '')
extlinks['r53-api-deep'] = (aws_docs_url + 'Route53/latest/APIReference/API_%s', '')
extlinks['s3-dg-deep']   = (aws_docs_url + 'AmazonS3/latest/dev/%s', '')
extlinks['ddb-dg-deep']  = (aws_docs_url + 'amazondynamodb/latest/developerguide/%s', '')

# usage: :mktplace-search:`Amazon Machine Images <AMISAWS>` where AMISAWS is the search term.
# usage: :mktplace-search:`Windows server <windows+server>` 
extlinks['mktplace-search'] = (aws_url + 'marketplace/search/results/&searchTerms=%s?browse=1', '')
extlinks['ec2-faq']         = (aws_url + 'ec2/faqs/%s', '')
extlinks['aws-articles']    = (aws_url + 'articles/%s', '')

# http://blogs.aws.amazon.com/net/post/Tx1P7UD2UN3DHK6/Overriding-Endpoints-in-the-AWS-SDK-for-NET
# pass in everything after post/
extlinks['aws-blogs-net'] = (aws_blogs_net + '%s', '')
extlinks['aws-blogs']     = (aws_url + 'blogs/%s', '')

