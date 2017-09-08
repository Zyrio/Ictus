Yiff.co API V2
==============

Documents for Yiff.co's API V2, at **https://yiff.co/api/v2/...**

#### Notes

 - No autentication is needed.
 - All returns are in JSON (REST).

### /api/v2/random/{tagName}

#### GET: /api/v2/random/{tagName}

##### Parameters

 - `{tagName}` The requested tag/repository to find files for

##### Return

```
{
	id: "46572208-96e8-4c61-67f3-08d460ae8f05",
	location: "https://yiff.fs.zyr.io/tumblr/tumblr_n4o0h1AhMC1swkv68o1_1280.jpg",
	file: {
		id: "46572208-96e8-4c61-67f3-08d460ae8f05",
		filename: "tumblr_n4o0h1AhMC1swkv68o1_1280.jpg",
		type: 0,
		originalFilename: "tumblr_n4o0h1AhMC1swkv68o1_1280.jpg",
		dateUploaded: "2017-03-01T14:25:56.919056",
		source: "tumblr"
	},
	tags: [
		{
			id: "a5be286c-afea-4322-76a3-08d460ae8f55",
			hits: 0,
			inUse: true,
			file: null,
			tag: {
				id: "0e0491a2-0cde-4a69-6cfd-08d460ae02bc",
				name: "furry",
				fileCount: 14209
			}
		}
	]
}
```

 - `id` The ID unique to an individual file
 - `location` The world-viewable URL of the file
 - `file`
   - `id` The ID unique to an individual file
   - `filename` The name of the file as stored on the server
   - `type` The type of file
   		- **0**: Image (JPG, JPEG, PNG)
   		- **1**: Animated (GIF)
   		- **2**: Video (MP4)
   - `originalFilename` The name of the file in its original form (usually indifferent from `filename`)
   - `dateUploaded` The date the upload completed, in UTC
   - `source` The "source" of the file, or the containing folder the physical file is in
- `tags` (collection)
	- `id` The ID unique to a tag->file link
	- `hits` (currently unused)
	- `inUse` (current unused)
	- `file` (unavailable in this call)
	- `tag`
		- `id` The ID unique to a tag/repository
		- `name` The name of the tag/repository
		- `fileCount` The amount of files currently held under this tag


### /api/v2/files

#### GET: /api/v2/files/{fileId}

##### Parameters

 - `{fileId}` The ID unique to an individual file

##### Return

```
{
	id: "46572208-96e8-4c61-67f3-08d460ae8f05",
	location: "https://yiff.fs.zyr.io/tumblr/tumblr_n4o0h1AhMC1swkv68o1_1280.jpg",
	file: {
		id: "46572208-96e8-4c61-67f3-08d460ae8f05",
		filename: "tumblr_n4o0h1AhMC1swkv68o1_1280.jpg",
		type: 0,
		originalFilename: "tumblr_n4o0h1AhMC1swkv68o1_1280.jpg",
		dateUploaded: "2017-03-01T14:25:56.919056",
		source: "tumblr"
	},
	tags: [
		{
			id: "a5be286c-afea-4322-76a3-08d460ae8f55",
			hits: 0,
			inUse: true,
			file: null,
			tag: {
				id: "0e0491a2-0cde-4a69-6cfd-08d460ae02bc",
				name: "furry",
				fileCount: 14209
			}
		}
	]
}
```

_This is the same output as **GET:/api/v2/random**_

 - `id` The ID unique to an individual file
 - `location` The world-viewable URL of the file
 - `file`
   - `id` The ID unique to an individual file
   - `filename` The name of the file as stored on the server
   - `type` The type of file
   		- **0**: Image (JPG, JPEG, PNG)
   		- **1**: Animated (GIF)
   		- **2**: Video (MP4)
   - `originalFilename` The name of the file in its original form (usually indifferent from `filename`)
   - `dateUploaded` The date the upload completed, in UTC
   - `source` The "source" of the file, or the containing folder the physical file is in
 - `tags` (collection)
 	- `id` The ID unique to a tag->file link
 	- `hits` (currently unused)
 	- `inUse` (current unused)
 	- `file` (unavailable in this call)
 	- `tag`
 		- `id` The ID unique to a tag/repository
 		- `name` The name of the tag/repository
 		- `fileCount` The amount of files currently held under this tag

#### POST: /api/v2/files

_(missing section)_

### /api/v2/tags

#### GET: /api/v2/tags

##### Parameters

(none)

##### Return

```
[
	{
		id: "7dca14f4-1619-4c47-76f4-08d460f952aa",
		name: "sports",
		fileCount: 15916
	},
	{
		id: "0e0491a2-0cde-4a69-6cfd-08d460ae02bc",
		name: "furry",
		fileCount: 14209
	},
	{
		id: "afe135d3-a624-49af-76f3-08d460f952aa",
		name: "shota",
		fileCount: 679
	},
...
```

#### POST: /api/v2/tags

Link a file to a tag, thus creating a new tag if the tag doesn't exist

##### Parameters

 - `{FileId}` The ID unique to an individual file
 - `{Name}` The name of a new, or existing tag/repository

###### Example

```
{
	"FileId":"3d3881c4-4193-4b92-0985-08d4f6a2a91b",
	"Name":"test2"
}
```

##### Return

(nothing)

### /api/v2/tags/default

#### GET: /api/v2/tags/default

Get the default **tag/repository** for the site.

##### Parameters

(none)

##### Return

```
{
	id: "0e0491a2-0cde-4a69-6cfd-08d460ae02bc",
	name: "furry",
	fileCount: 14209
}
```

- `id` The ID unique to a tag/repository
- `name` The name of the tag/repository
- `fileCount` The amount of files currently held under this tag

### /api/v2/files/uploads

#### POST: /api/v2/files/uploads

##### Parameters

(none, requires a physical file)

##### Return

```
{
    "id": "3d3881c4-4193-4b92-0985-08d4f6a2a91b"
}
```

 - `id` The ID of the newly created file