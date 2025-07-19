# 🎬 StreamWeaver

**Automate your livestream workflow—from multi-platform broadcasts to polished blog posts, short clips, and social media assets.**

StreamWeaver is a serverless, event-driven automation toolkit built with Azure Functions, Blazor, and .NET Aspire. It ingests streams from platforms like YouTube, LinkedIn, and Twitch, processes transcripts and video blobs, and generates repurposed content through modular pipeline components—all visualized through a real-time dashboard.

---

## ⚡ Features

- 🔄 **Event-driven architecture** using Azure Queues, Table Storage, and SignalR
- 🧠 **Transcript ingestion** via speech-to-text + video/audio blob management
- ✍️ **Content generation** for blog drafts, short clips, and captions
- 📣 **Notification center** with live updates via SignalR
- 🎨 **Blazor SPA frontend** for stream monitoring and export control
- 🧪 **Test suite** for functions, logic modules, and dashboard UI
- 🔍 **Orchestrated locally with .NET Aspire** for full observability and dev ergonomics

---

## 🗂️ Project Structure

```plaintext
src/
├── StreamDashboard/         # Blazor SPA frontend
├── StreamProcessor/         # Azure Function: downloads transcript + blob
├── ContentGenerator/        # Azure Function: generates blog, clips, captions
├── NotificationService/     # Azure Function: SignalR and alert dispatch
├── StreamApi/               # Optional ASP.NET API for metadata
├── SharedModels/            # DTOs, common logic, bindings
├── AppHost/                 # .NET Aspire orchestration
└── AppHost.Components/      # Aspire resource/service extensions

tests/
├── StreamProcessor.Tests/
├── ContentGenerator.Tests/
├── NotificationService.Tests/
├── StreamDashboard.Tests/
└── SharedModels.Tests/
