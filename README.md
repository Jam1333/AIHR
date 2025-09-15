# AI HR Assistant 🤖

A cutting-edge AI-powered solution designed to revolutionize the recruitment process through intelligent automation of resume screening and candidate interviews.

![Docker](https://img.shields.io/badge/Docker-Compose-blue?style=flat&logo=docker)
![React](https://img.shields.io/badge/React-18.x-blue?style=flat&logo=react)
![ASP.NET](https://img.shields.io/badge/ASP.NET_Core-9.0-purple?style=flat&logo=dotnet)
![TypeScript](https://img.shields.io/badge/TypeScript-5.x-blue?style=flat&logo=typescript)
![MongoDB](https://img.shields.io/badge/MongoDB-7.x-green?style=flat&logo=mongodb)

## 🌟 Overview

AI HR Assistant addresses critical challenges in modern recruitment:
- **Time Efficiency**: Reduces manual screening time by up to 40%
- **24/7 Availability**: Conducts interviews anytime, anywhere
- **Objective Assessment**: Eliminates human fatigue and bias
- **Data-Driven Decisions**: Provides quantifiable candidate matching

## ✨ Key Features

### 🎯 Intelligent Candidate Screening
- Automated resume analysis and filtering
- Skills and experience matching against job requirements
- Dynamic interview question generation

### 🗣️ Voice-Driven Interviews
- Real-time speech recognition with multi-language support
- Adaptive questioning based on previous responses
- Emotional tone and pause analysis for soft skills assessment

### 📊 Advanced Analytics
- NLP-powered response evaluation
- Customizable scoring weights for different competencies
- Contradiction detection and red flag identification

### 📝 Comprehensive Reporting
- Percentage-based position matching
- Detailed competency breakdown
- Personalized candidate feedback
- Actionable recommendations (advance/reject/request clarification)

## 🏗️ System Architecture

```mermaid
graph TD
    A[React Client] <--> B[ASP.NET Core API]
    B <--> C[MongoDB]
    B <--> D[Ollama AI]
    E[Mongo Express] <--> C
```

## 🚀 Quick Start

### Prerequisites
- Docker & Docker Compose
- GPU support (recommended for Ollama)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/Jam1333/AIHR.git
cd aihr
```

2. Start the application:
```bash
docker-compose up -d
```

3. Access the services:
- Frontend: http://localhost
- Backend API: http://localhost:5000
- Mongo Express: http://localhost:8081

## 📁 Project Structure

```
src/
├── client-app/          # React frontend
│   ├── src/
│   ├── public/
│   └── Dockerfile
├── backend/
│   └── AIHR.Api/       # ASP.NET Core Web API
│       └── Dockerfile
docker-compose.yml
```

## ⚙️ Configuration

### Environment Variables

**Backend Configuration:**
```yaml
ConnectionStrings__Database: mongodb://admin:admin@mongo:27017/
ConnectionStrings__Ollama: http://ollama:11434
```

**MongoDB Configuration:**
```yaml
MONGO_INITDB_ROOT_USERNAME: admin
MONGO_INITDB_ROOT_PASSWORD: admin
```

## 🎮 Usage

1. **Setup Job Requirements**: Define position criteria through the admin interface
2. **Upload Resumes**: System automatically screens and ranks candidates
3. **Schedule Interviews**: AI avatar conducts adaptive interviews 24/7
4. **Review Results**: Access detailed reports and candidate evaluations
5. **Provide Feedback**: Automated personalized feedback sent to candidates

## 🔧 Customization

### Adjusting Scoring Weights
Modify competency weights in the HR interface:
- Technical Skills: 50%
- Communication: 30%
- Case Studies: 20%

## 🤝 Contributing

We welcome contributions!

## 🆓 Support

For support and questions:
- Open an issue on GitHub

## 🔍 Showcase

![showcase photo](/img/home.jpg)

![showcase photo](/img/profile.jpg)

![showcase photo](/img/vacancies.jpg)

![showcase photo](/img/create-vacancy.jpg)

![showcase photo](/img/vacancy.jpg)

![showcase photo](/img/create-analysis.jpg)

![showcase photo](/img/analysis.jpg)

![showcase photo](/img/create-interview.jpg)

![showcase photo](/img/interview.jpg)
