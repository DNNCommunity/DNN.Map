# DNN.Map

DotNetNuke&#174; Map is a module which provides a mashup for any custom data into the Google Map API for displaying informative maps on your DNN site.

## Warning: Abandonware/antiqueware

This module was last released in 2008. It needs to be updated to work today but the original developers have moved on. There are two aspects to this:

1. It was built in VB.NET using the old DNN Web Forms module pattern. Not only is this technically outdated, but it means it doesn't provide a modern user experience. The visual language is outdated and the many postbacks make working with it slow.
2. Google has moved on. The code will need to be updated to leverage the latest Google API requirements.

## A decision must be made

Repairing the module to satisfy #2 is probably doable if someone invests enough time to delve into how things work and repairs this. Acting on #1 above will be a more major investment.

The big question is: is it all worth it? There are several existing solutions that cover this domain:

1. There is a (simpler) open source Map module over at DNN Connect: https://github.com/DNN-Connect/Map (This module does the basics for a map module, allowing the display of a Google map and setting markers on it).
2. There are free alternatives to render points from a data source on a map (e.g. 2sxc).
3. There is a whole slew of commercial map modules for DNN on the DNN Store.

## So the main questions are as follows

1. Is the current feature set of this module unique and valuable enough to invest more time in?
2. Is there still a "critical mass" for this module? I.e. is anyone still using it.
3. Who is willing to take on the tasks above? Should we crowdsource development for this?

Absent of a "yes" to the above questions we will move ahead and replace this module with the DNN Connect variant so that we don't spread our resources to thin.

