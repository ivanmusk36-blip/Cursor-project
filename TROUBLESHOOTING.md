# Troubleshooting Guide: 404 Error with Grok API

## Why You're Getting a 404 Error

The 404 error on line 59 of `InfiniteTestService.cs` can happen for several reasons:

### **🔍 Step-by-Step Analysis**

#### **Step 1: API Endpoint Issues**
- **Problem**: The endpoint `https://api.x.ai/v1/chat/completions` might be incorrect
- **Solution**: The code now tries multiple endpoints automatically

#### **Step 2: API Key Issues**
- **Problem**: Your API key might not be properly configured
- **Check**: Run `dotnet user-secrets list` to verify your key is set
- **Format**: x.ai API keys should start with `xai_`

#### **Step 3: Model Name Issues**
- **Problem**: The model name `"grok-beta"` might be incorrect
- **Solution**: Check the latest x.ai API documentation for correct model names

#### **Step 4: Request Format Issues**
- **Problem**: The JSON request format might not match what x.ai expects
- **Solution**: The code now includes better error handling

## **🔧 How to Debug**

### **1. Check Your API Key**
```bash
cd EnglishLearningApp.Presentation
dotnet user-secrets list
```

You should see:
```
testGrokAPIKey = xai_your_actual_key_here
```

### **2. Verify API Key Format**
- Your API key should start with `xai_`
- It should be about 40-50 characters long
- No spaces or special characters at the beginning/end

### **3. Test API Key Manually**
You can test your API key using curl:
```bash
curl -X POST "https://api.x.ai/v1/chat/completions" \
  -H "Authorization: Bearer YOUR_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "model": "grok-beta",
    "messages": [{"role": "user", "content": "Hello"}],
    "max_tokens": 100
  }'
```

### **4. Check Console Output**
The application now logs API errors to the console. Look for messages like:
- "API key not properly configured, using fallback test"
- "API call failed: [error details]"

## **🚀 Quick Fixes**

### **Fix 1: Update Your API Key**
```bash
dotnet user-secrets remove "testGrokAPIKey"
dotnet user-secrets set "testGrokAPIKey" "your_actual_xai_key_here"
```

### **Fix 2: Check x.ai Documentation**
Visit [x.ai API documentation](https://docs.x.ai) to verify:
- Correct endpoint URLs
- Valid model names
- Request format requirements

### **Fix 3: Test with Fallback**
The application automatically falls back to hardcoded questions if the API fails, so you can still test the feature.

## **📋 Common Issues**

### **Issue 1: "API key not configured"**
- **Cause**: User secret not set or wrong name
- **Fix**: Set the secret correctly with `dotnet user-secrets set`

### **Issue 2: "404 Not Found"**
- **Cause**: Wrong endpoint URL
- **Fix**: The code now tries multiple endpoints automatically

### **Issue 3: "401 Unauthorized"**
- **Cause**: Invalid API key
- **Fix**: Check your API key format and validity

### **Issue 4: "400 Bad Request"**
- **Cause**: Invalid request format
- **Fix**: Check the JSON structure in the request

## **🔍 Debugging Steps**

1. **Check the console output** when running the application
2. **Verify your API key** is correctly set
3. **Test the API manually** with curl or Postman
4. **Check x.ai documentation** for the latest API specifications
5. **Try the fallback mode** to ensure the feature works without the API

## **📞 Getting Help**

If you're still having issues:
1. Check the console output for specific error messages
2. Verify your x.ai account has API access
3. Contact x.ai support for API-specific issues
4. The application will work with fallback questions even if the API fails 