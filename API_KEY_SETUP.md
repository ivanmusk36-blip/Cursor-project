# API Key Setup Guide

## Setting up the Grok API Key

The application uses .NET User Secrets to securely store the Grok API key. This ensures that sensitive information is not exposed in source code or configuration files.

### Step 1: Get your Grok API Key

1. Visit [x.ai](https://x.ai) and sign up for an account
2. Navigate to your API settings
3. Generate a new API key
4. Copy the API key (it will look something like `xai_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx`)

### Step 2: Configure User Secrets

1. Open a terminal/command prompt
2. Navigate to the Presentation project directory:

   ```bash
   cd EnglishLearningApp.Presentation
   ```

3. Set the API key using user secrets:

   ```bash
   dotnet user-secrets set "testGrokAPIKey" "your-actual-api-key-here"
   ```

   Replace `"your-actual-api-key-here"` with your actual Grok API key.

### Step 3: Verify the Setup

1. You can verify that the secret was set correctly by running:

   ```bash
   dotnet user-secrets list
   ```

2. You should see output like:
   ```
   testGrokAPIKey = your-actual-api-key-here
   ```

### Step 4: Run the Application

1. Build and run the application:

   ```bash
   dotnet run
   ```

2. Navigate to the lessons page and try the "Infinite Test" feature

### Security Notes

- **User Secrets are local**: They are stored in your user profile and are not committed to source control
- **Development only**: User secrets are only loaded in development environment
- **No hardcoded values**: The API key is never stored in source code or configuration files
- **Environment variables**: For production, use environment variables or Azure Key Vault

### Troubleshooting

If you get an error about the API key not being configured:

1. Make sure you've set the user secret correctly
2. Verify the secret name is exactly `"testGrokAPIKey"`
3. Restart the application after setting the secret
4. Check that you're running in development mode

### For Production

In production environments, you should:

1. Use environment variables:

   ```bash
   export testGrokAPIKey="your-api-key"
   ```

2. Or use Azure Key Vault for Azure deployments
3. Never commit API keys to source control
