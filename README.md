# **StaticBlazePOC - Blazor WASM GitHub Integration**

A Proof of Concept (PoC) project demonstrating how to use **Blazor WebAssembly (WASM)** to interact with the **GitHub API** for creating and managing markdown blog posts and images in a GitHub repository.

---

## **Features**
- **Push Markdown Files**: Create and update markdown files in a GitHub repository.
- **Push Image Files**: Upload images to a GitHub repository.
- **Static Site Generator**: Designed for use with static site generators like Jekyll or Hugo.
- **GitHub Pages Integration**: Automatically deploy changes to GitHub Pages.

---

## **Prerequisites**
1. **GitHub Account**: Create a repository for testing.
2. **GitHub PAT**: Generate a Personal Access Token with `repo` scope.
3. **.NET SDK**: Install the latest .NET SDK (8.0 or later).

---

## **Setup**

### **1. Clone the Repository**
```bash
git clone https://github.com/SaintScraTchY/StaticBlazePOC.git
cd StaticBlazePOC
```

### **2. Configure GitHub PAT**
- Generate a GitHub PAT with `repo` scope.
- Add the PAT to the app:
  - Open `Pages/MarkdownUpload.razor` and `Pages/ImageUpload.razor`.
  - Replace `your-username`, `your-repo`, and `your-pat` with your GitHub details.

### **3. Run the Project**
```bash
dotnet run
```
- Navigate to `https://localhost:7210` in your browser.

---

## **Usage**

### **1. Upload Markdown**
- Go to `/markdown-upload`.
- Enter:
  - **File Path**: `posts/test.md`
  - **Markdown Content**: `# Hello, World!`
  - **GitHub PAT**: Your GitHub PAT.
- Click **Upload**.

### **2. Upload Images**
- Go to `/image-upload`.
- Select an image file.
- Enter:
  - **File Path**: `images/test.jpg`
  - **GitHub PAT**: Your GitHub PAT.
- Click **Upload**.

---
## **Code Structure**
```
StaticBlazePOC/
├── Pages/
│   ├── MarkdownUpload.razor       # Markdown upload UI
│   ├── ImageUpload.razor          # Image upload UI
├── Services/
│   ├── GitHubService.cs           # GitHub API service
├── Program.cs                     # HttpClient configuration
├── README.md                      # This file
```

---

## **Example API Request**
### **Create Markdown File**
```http
PUT https://api.github.com/repos/SaintScraTchY/StaticBlazePOC/contents/posts/test.md
Authorization: Bearer YOUR_PAT
User-Agent: BlazorGitPOC
Content-Type: application/json

{
    "message": "Add posts/test.md",
    "content": "SGVsbG8sIFdvcmxkIQ==",
    "branch": "main"
}