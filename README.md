### AES_Complete Provides 
- Provides encryption & decryption using AES256 algorithm and implementing Authenticated encryption.
- Authenticated encryption uses a new IV value every time it encrypts the data.
- Authenticated encryption produces an HMAC over the data:iv so you can be sure when decrypting the data that it has not been hacked or corrupted.
1. Authenticated* encryption using AES256
2. Decryption of AES256 data


### AES_Complete Used With CYaPass-Avalonia Project
Please see my other project which uses this Library: https://github.com/raddevus/CYaPass-Avalonia
### Tests: Check The Test Project To Learn Usage
You can run `$ dotnet test` to see everything run.

### Basic Usage

