import React, { useEffect, useState } from "react";
import { getProducts } from "./api";

function App() {
  const [products, setProducts] = useState([]);
  const [error, setError] = useState("");

  useEffect(() => {
    getProducts()
      .then(setProducts)
      .catch(() => setError("Could not load products"));
  }, []);

  return (
    <div style={{ padding: "20px", fontFamily: "Arial" }}>
      <h1>3-Tier Azure Devops Demo App </h1>
      <h2>self hosted runner and Gitops!</h2>
      <p>React frontend + ASP.NET Core API + Azure SQL + Key Vault</p>

      {error && <p>{error}</p>}

      <ul>
        {products.map((p) => (
          <li key={p.id}>
            {p.name} - ${p.price}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;