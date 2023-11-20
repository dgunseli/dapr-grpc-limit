- Run the application

```
dapr run -f .
```

- Call the test api

```
curl --location 'http://localhost:5010/test' \
--form 'imageData=@"sample-5mb-image.jpg"'
```