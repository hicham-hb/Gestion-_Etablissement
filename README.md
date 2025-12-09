# Gestion-_Etablissement
<<<<<<< HEAD
## 🎯 Objectif du projet

Ce projet a pour objectif de fournir une application  permettant de gérer la vie scolaire :  
- conception des emplois du temps,  
- accès à un espace personnel pour chaque utilisateur (directeur,élève, professeur),  


L’application doit être simple à utiliser, sécurisée et accessible sur différents appareils .

---

##  Personae & User Stories

###  Directeur / Administrateur
> *En tant que directeur, je veux concevoir les emplois du temps de chaque classe pour la rentrée.*

Fonctionnalités associées :
- Concevoir la base de données pour :
 
  - les classes,
  - les matières,
  - les créneaux horaires.



> *En tant qu’admin, je veux gérer les comptes utilisateurs.*

Fonctionnalités associées :
- Ajouter un rôle/profil utilisateur ( professeur, élève,).
- Gérer la création, modification et suppression de comptes.
- Sécuriser l’accès aux différentes sections de l’application.

---

### Professeur
> *En tant que professeur, je veux avoir accès à mon emploi du temps personnalisé.*

Fonctionnalités associées :
- Accéder à un planning personnel filtré par professeur.
- Affichage clair des créneaux (matière, salle, classe).


---

###  Élève
> *En tant qu’élève, je veux consulter mon emploi du temps facilement.*

Fonctionnalités associées :

- Créer une vue élève avec filtre par classe.


> *En tant qu’élève, je veux accéder à mon espace pour voir mes notes, emploi du temps, messages.*

Fonctionnalités associées :

- Développer les modules :
  - Notes,
  - Messages,
  - Emploi du temps.
- Intégrer le système de login et d’authentification.
- Sécuriser les accès aux données personnelles.

---


##  Modules principaux

1. **Module Emplois du temps**
   - Modélisation des classes, cours, professeurs, créneaux horaires.
   - Interface d’administration pour créer et modifier les plannings.
   
2. **Espaces personnels**
   - Espace élève : notes, emploi du temps, messages.
   - Connexion via système d’authentification (login/mot de passe).
   - Gestion sécurisée des sessions et des rôles.

3. **Module d’assiduité**
   - Gestion des profils utilisateur .
  

---

##  Authentification & Sécurité

- Implémentation d’une **authentification** pour les profils :
  - Administrateur
  - Professeur
  - Élève
  
- Gestion des autorisations selon le rôle.
- Sécurisation des données .

---

# Technologies utilisées

Langage : C# (.NET 8 / .NET 6)

Framework : WPF (Windows Presentation Foundation)

Architecture : MVVM (CommunityToolkit.Mvvm)

Base de données : SQLite

ORM : Entity Framework Core

IDE : Visual Studio 2022
=======
Gestion d'un établissement secondaire
>>>>>>> b3c787742f7b4078f113deac953d2774a0853782
