# Markdown To Steam Utility

Converts a Markdown formatted file to Steam's proprietary BBCode format.  


# Need
This tool is different from many of the other Markdown to Steam BBCode converters as it parses the text using the Markdig library rather than RegEx.  Markdig is a DOM parser which makes Markdown parsing much more accurate.

# Usage

```
MarkdownToSteam 2.0.0
Copyright (C) 2023 MarkdownToSteam

  -i, --input                     Required. The full path to the ReadMe.md file to parse.

  -o, --output                    The file to output the result to.  If not provided, will output to the console.

  -r, --remove-relative-images    Removes images that have a relative path

  -b, --base-url                  (Default: ) Any relative URI's will be converted to absolute URLs using this URL as the base.

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
Image links can be processes in a couple of ways:

### Remove
Using the -r option, relative images links will be completely removed.  

This can be used to avoid relying on a resource external to Steam and avoid needing to remove the links manually.  The user would normally upload the images to the Mod's images area.

### Absolute Uri Base Path
Using the -b option, the user can provide a base URI.  That URI will be combined with any relative image URIs to create an absolute URI.

For example, directly linking github images:

Base URI:
```https://raw.githubusercontent.com/SomeUser/SomeRepo/master/```

Markdown image relative link:
```![Counters Example](media/Example%20Diagram.png)```

BBCode Result:
```[img]https://raw.githubusercontent.com/SomeUser/SomeRepo/master/media/Example%20Diagram.png[/img]```



## Plain Links
A link which is either plain text or use the same text for the link and the literal will not be rendered as a Steam `[url]` tag.

For example, ```https://example.com``` and ```(https://example.com)[https://example.com]``` will both be rendered as ```https://example.com```

There is no visual or functional difference in the Steam translation.

# Additional Elements
If there is a Markdown element that is not translated, feel free to create an issue or contribute to this repo.
