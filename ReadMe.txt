Mi sono permesso del potere decisionale riguardo alcuni punti per me poco chiari, e per mancanza di tempo e capacit�
ho dovuto apportare delle piccole modifiche:
- Non � specificato cosa accade al contatto tra player e nemici nel caso il player non stia attaccando. Ho allora presupposto
  che qualora dovesse verificarsi, il player subisca l'attacco. Tutto ci� per evitare che il giocatore abbia
  la possibilit� di stare immobile al centro e cambiare colore quando necessario senza mai dover di fatto attaccare.
- Ho aggiunto un semplice score che tiene conto dei nemici uccisi.
- Lo scaling della difficolt� nel tempo non � il mio forte, ci si dovrebbe basare su semplici funzioni matematiche che
  mi avrebbero richiesto ricerche online. Per questo lo scaling della velocit� specificato (il doppio ogni tot secondi) 
  non � esatto in game.
- Ho deciso di usare uno spawn delay tra ogni nemico di 4 secondi che diminuisce col tempo fino ad un massimo di 1 secondo
  tra ogni spawn.