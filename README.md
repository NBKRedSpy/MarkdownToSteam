# Markdown To Steam Utility

Converts a Markdown formatted file to Steam's proprietary BBCode format.  


# Need
This tool is different from many of the other Markdown to Steam BBCode converters as it parses the text using the Markdig library rather than RegEx.  Markdig is a DOM parser which makes Markdown parsing much more accurate.

# Usage

```
MarkdownToSteam 1.3.0
Copyright (C) 2023 MarkdownToSteam

  -i, --input                     Required. The full path to the ReadMe.md file to parse.

  -o, --output                    The file to output the result to.  If not provided, will output to the console.

  -m, --render-relative-images    (Default: false) Does not remove images that have a relative path.

  --help                          Display this help screen.

  --version                       Display version information.
  ```


# Steam Formatting Differences

## Inline Code Blocks

Markdown inline code blocks are rendered as italic text since Steam only supports multiline code blocks.

|||
|--|--|
|Markdown source|example ```inline code``` example|
|Steam output|example *inline code* example|


## Relative Image Links
By default, images that have a relative path such as ```![example](./media/example.png)``` are automatically removed.  However, this can be overridden by using the `--render-relative-images` option (or `-m` shorthand).  

If retained, the user will need to manually change the URL in the Steam output to an absolute path.  Alternatively, the user could change the Markdown source to an absolute path.

Relative images are removed by default since a common course of action is to remove the nonfunctional relative images.  

## Plain Links
Plain text links will not be wrapped in a Steam "`[url]`" tag.  

There is no visual or functional difference in the Steam translation.

For example, a plain text link is ```https://example.com```, while a Markdown link is ```[example](https://example.com)```.  The latter will be translated to a `[url]` tag.


# Additional Elements
If there is a Markdown element that is not translated, feel free to create an issue in this repository.
