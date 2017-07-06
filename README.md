# PdfShrinker
We needed a very simple way for staff to compress their PDF documents down so they could be sent via email.
Adobe Distiller was overkill, and other tools involved too many steps or required the original documents to
be modified. So to meet their needs I quickly put this small tool together.

This application uses [Ghostscript](https://www.ghostscript.com) to handle the compression. You will need to have a version
installed in order to use this tool. If you already use [PDF Creator](http://www.pdfforge.org/pdfcreator) or 
[CutePDF](http://www.cutepdf.com/) then GhostScript is probably already installed.

For Word and Publisher files the [Office Interop libaries](https://msdn.microsoft.com/en-us/library/15s06t57.aspx) are used
to automate those applications to save your documents as PDFs before compressing them.

## Usage

Simply drag some PDF or Word or Publisher files you'd like compressed to PDF and it will spit out 
compressed versions in the same directory as the originals. See below:

![Drag and Drop](http://i.imgur.com/64PCuYo.gif)

You can also press the *Open* button to browse for files using the regular Open File Dialog.

## Requirements
You'll need Ghostscript installed. If you don't have a copy it can be downlaoded from 
https://www.ghostscript.com/download/gsdnld.html

To convert Word and Publisher files you'll Microsoft Word and/or Publisher installed on your PC. 
Trial versions are available from https://products.office.com/en-au/try.

## Download
Binary releases available on https://github.com/pgodwin/PdfShrinker/releases

For source-code, this is Github - you're looking at the source!

## Future Enhacements
I'm not looking at adding any features to the application anytime soon. However some useful ideas are:
 - Configurable compression setting
 - Error handling/graceful failing if conversion/compression fails
 - Command-line options for batch jobs

## License
As per Ghostscript requirements, this application is also licensed under the terms of the AGPL.

