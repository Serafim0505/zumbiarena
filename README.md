# ZUMBI ARENA — PROJETO PRÁTICO (P2)
**Disciplina:** Desenvolvimento de Jogos Digitais  
**Curso:** Ciência da Computação  
**Engine:** Unity 6 (`6000.5.7f1`)  
**Ano:** 2026  

---

## 👥 Integrantes da Equipe
* **Felipe Amorim**
* **Gabriel Serafim**
* **Kayo Ferreira**

---

## 🎮 Sobre o Jogo
**Zumbi Arena** é um jogo de ação e sobrevivência 2D no estilo *Top-Down Shooter*. O jogador assume o papel de um sobrevivente preso no interior de uma casa cercada por mortos-vivos.

O jogo conta com dois modos selecionáveis no Menu Principal:
1. **Modo Desafio (História / Fuga):** Sobreviva a 10 rodadas intensas contra hordas crescentes. Ao abater todos os zumbis da 10ª onda, a porta se abre e o jogador deve correr até a saída para escapar com vida.
2. **Modo Arena (Infinito):** Sobrevivência arcade tradicional com ondas ilimitadas para alcançar a maior pontuação possível.

---

## 🕹️ Controles (PC)

| Ação | Tecla / Botão |
| :--- | :--- |
| **Movimentação do Sobrevivente** | `W`, `A`, `S`, `D` |
| **Mirar / Rotacionar Personagem** | `Movimento do Mouse` (360º) |
| **Atirar** | `Botão Esquerdo do Mouse (LMB)` |
| **Recarregar Arma** | `R` (ou automático ao zerar o pente) |
| **Equipar Rifle de Assalto (Automático)** | Tecla `1` |
| **Equipar Pistola 9mm (Semiautomática)** | Tecla `2` |
| **Comprar Suprimentos nas Caixas** | Tecla `E` (dentro do raio de proximidade) |
| **Pausar Jogo** | `Esc` ou `P` |

---

## ⚙️ Instruções de Execução

### Opção 1: Executando a Build Final (.exe)
1. Extraia o arquivo compactado `ZumbiArena_Build.zip` em uma pasta de sua preferência.
2. Dê um duplo clique no executável **`ZumbiArena.exe`**.
3. O jogo iniciará diretamente pelo Menu Principal em resolução de tela cheia.

### Opção 2: Executando pelo Editor da Unity
1. Abra o **Unity Hub** e clique em **Add > Add project from disk**.
2. Selecione a pasta raiz do projeto: `ZumbiArena`.
3. Abra o projeto utilizando a versão **Unity 6**.
4. No painel de arquivos (*Project*), navegue até `Assets/Scenes` e abra a cena **`Menu.unity`**.
5. Clique no botão **Play (▶)** no topo da interface da Unity.

---

## 🛠️ Tecnologias e Mecânicas Implementadas
* **Física Realista (Physics2D):** Movimentação vetorial no `FixedUpdate` via `Rigidbody2D`, colisões de projéteis e sistema balístico de *Knockback* (empurrão por impacto) com dissipação de energia quadrática por distância no Rifle e momento fixo à queima-roupa na Pistola.
* **Inteligência Artificial (Pathfinding):** Algoritmo de busca de rotas em grade 2D que contorna quinas e paredes do cenário sem atravessar colisores sólidos.
* **Economia de Recursos:** 3 caixas de suprimentos no cenário com detecção de aproximação vetorial (`Vector2.Distance`), permitindo gastar pontos por abate em munição de rifle, pistola e kits de cura.
* **Sonorização:** Efeitos de disparos, passos, danos, recargas mecânicas e gemidos de morte vinculados a eventos físicos.

---

## 📜 Créditos de Assets de Terceiros
Todos os assets de terceiros utilizados são gratuitos e de domínio público ou licença aberta:
* **Assets Gráficos:** Kenney.nl (*Top-Down Shooter & Urban Assets* — Licença CC0 1.0 Universal).
* **Efeitos Sonoros:** Freesound.org Community (*Footsteps, Zombie Grunts & Impacts* — Licença Creative Commons).
