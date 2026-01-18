# Estatia

Real Estate, Real Easy 

##  Description

Estatia is a modern **ASP.NET Core** real estate management system designed to streamline the connection between agents, buyers, and administrators. It features a robust property marketplace, role-based security, and an integrated **Machine Learning Price Estimator**.

## ✨ Features

- **Property Marketplace:** Search, filter, and view properties in real-time.
- **ML Price Estimator:** Predict property values using a trained Machine Learning model.
- **Role-Based Access Control:** Distinct dashboards for Admins, Agents, and Users.
- **Responsive Design:** Built with Bootstrap for seamless mobile and desktop use.

## 🛠 Tech Stack

- **Framework:** ASP.NET Core (Web SDK)
- **Database:** SQLite / Entity Framework Core
- **Machine Learning:** ONNX Runtime, Scikit-Learn (Python)
- **Frontend:** Razor Views, Bootstrap
- **Security:** BCrypt Hashing

---

![Estatia Banner](Estatia.png)

##  How the ML Model Works

Estatia uses a **Linear Regression Model** to estimate property prices based on historical data.

1.  **Training (Python):**
    - A Python script processes dataset features: `Size (SqFt)`, `City`, and `Listing Type`.
    - It trains a Linear Regression model using **Scikit-Learn**.
    - The model is exported to **ONNX (Open Neural Network Exchange)** format for compatibility.

2.  **Inference (.NET):**
    - The application uses the `Microsoft.ML.OnnxRuntime` library to load the `.onnx` file.
    - A dedicated **Singleton Service** holds the model in memory for fast performance.
    - When a user clicks "Predict", the C# backend feeds the inputs into the model and returns the predicted price instantly.

---




### 1. Clone the Repository
```bash
git clone [https://github.com/aabdullahsayed/Estatia.git](https://github.com/aabdullahsayed/Estatia.git)
cd Estatia