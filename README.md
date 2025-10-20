# 🧩 Modular Unity Shop — Test Task

> Clean, domain-driven Unity prototype with isolated business domains and flexible ScriptableObject-based “bricks”.

---

## 🚀 Overview

This project implements a **simple in-game shop** with full **domain isolation** according to the task requirements.

Each domain (Health, Gold, Location, VIP, Shop) is completely independent and connected only through the **Core** domain.

> 💡 All business logic lives inside its own domain.  
> The *Shop* domain doesn’t know anything about Health, Gold, or VIP — it only operates via `IOperation` interfaces from `Core`.

---

## 🧱 Architecture

Assets/

├── Core/ → Base abstractions (IOperation, PlayerData, DomainEvents)

├── Health/ → HP domain: costs & rewards (fixed / percent)

├── Gold/ → Gold domain: add / spend

├── Location/ → Player location domain

├── VIP/ → VIP domain: time-based buffs (seconds)

└── Shop/ → Shop UI + logic (bundles, async purchase)

---

## 🧩 Available Bricks
Domain	Type	Description
Gold	CostGoldSO, RewardGoldSO	Spend / add fixed gold
Health	CostFixedHpSO, RewardFixedHpSO	Spend / gain fixed HP
	CostPercentHpSO, RewardPercentHpSO	Spend / gain % of HP
Location	RewardSetLocationSO	Change current location
VIP	CostVipSecondsSO, RewardAddVipSO	Spend / gain VIP time in seconds
