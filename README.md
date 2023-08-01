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
By default, images that have a relative path are automatically removed.  This is to handle
the common GitHub relative path image linking, which is often simply removed.

Use the `--render-relative-images` option (or `-m` shorthand) to force the links to be rendered.

## Plain Links
Plain links that are written in plain text not be rended as a "`[url]`"

For example, a plain text link would be `https://example.com` while a Markdown link is `[example](https://example.com)`

The reason is that Steam will flag links such as GitHub as dangerous and prevent navigation.  To force a link to be rendered as a `[url]`, change the Markdown source's url to `[]()` format.

