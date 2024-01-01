
# GIT Users

An API to retrieve GIT users details by names, using the endpoint 
`https://api.github.com/users/{username}`

## API Reference

### Retrieve Users

```http
  GET /retrieveUsers
```

#### **Parameters**

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `userNames` | `string array` | `?userNames=name1&userNames=name2` |


#### **Responses**
| Code | Description   |
| :-------- | :------- | 
| `200` | `Success`    |
| `500` | `Server Error`    |

##### *Response Body Schema*
```
[
  {
    "login": "string",
    "name": "string",
    "company": "string",
    "followers": 0,
    "public_repos": 0,
    "average_followers": 0
  }
]
```


